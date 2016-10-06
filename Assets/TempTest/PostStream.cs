using UnityEngine;
using System.Collections.Generic;
using System.Text;


public class PostStream
{
    // 保存域头
    public Dictionary<string, string> Headers = new Dictionary<string, string>();

    // 末尾16个字节保存md5数字签名
    const int HASHSIZE = 16;

    // byte占一个字节
    const int BYTE_LEN = 1;

    // short 占2个字节
    const int SHORT16_LEN = 2;

    // int 占4个字节
    const int INT32_LEN = 4;

    // int 占4个字节
    const int FLOAT_LEN = 4;

    private int m_index = 0;
    public int Length { get { return m_index; } }


    // 秘密密码,用于数字签名
    private string m_secretKey = "123456";


    // 存储Post信息
    private string[,] m_field;

    private const int MAX_POST = 128;
    private const int PAIR = 2;
    private const int HEAD = 0;
    private const int CONTENT = 1;

    // 收到的字节数组
    private byte[] m_bytes = null;
    public byte[] BYTES { get { return m_bytes; } }

    // 发送的字符串
    private string m_content = "";

    // 读取是否出现错误
    private bool m_errorRead = false;

    // 是否进行数字签名
    private bool m_sum = true;


    public PostStream()
    {
        Headers = new Dictionary<string, string>();

        m_index = 0;

        m_bytes = null;

        m_content = "";

        m_errorRead = false;
    }


    public void BeginWrite(bool issum)
    {
        m_index = 0;

        m_sum = issum;

        m_field = new string[MAX_POST, PAIR];
        /* "Content-Type", "application/x-www-form-urlencoded"——Form表单语法
		 * 
		 * 在Form元素的语法中，EncType表明提交数据的格式 用 Enctype 属性指定将数据
		 * 回发到服务器时浏览器使用的编码类型。 
		 * 例如： application/x-www-form-urlencoded： 窗体数据被编码为名称/值对。
		 * 
		 * 这是标准的编码格式。 multipart/form-data： 窗体数据被编码为一条消息，页
		 * 上的每个控件对应消息中的一个部分，这个一般文件上传时用。 text/plain： 窗
		 * 体数据以纯文本形式进行编码，其中不含任何控件或格式字符。 
		 * 
		 * form的enctype属性为编码方式，常用有两种：
		 * application/x-www-form-urlencoded和multipart/form-data，默认为
		 * application/x-www-form-urlencoded。 
		 * 
		 * 1.x-www-form-urlencoded：
		 * 当action为get时候，浏览器用x-www-form-urlencoded的编码方式把form数据
		 * 转换成一个字串（name1=value1&name2=value2…），然后把这个字串append到
		 * url后面，用?分割，加载这个新的url。
		 * 
		 * 2.multipart/form-data
		 * 当action为post时候，浏览器把form数据封装到http body中，然后发送到server。
		 * 如果没有type=file的控件，用默认的application/x-www-form-urlencoded就可
		 * 以了。 但是如果有type=file的话，就要用到multipart/form-data了。浏览器会
		 * 把整个表单以控件为单位分割，并为每个部分加上
		 * Content-Disposition(form-data或者file),Content-Type(默认为text/plain)
		 * ,name(控件name)等信息，并加上分割符(boundary)。
		 * 
		 * 通过POST方式向服务器发送请求时最好要通过设置请求头来指定为
		 * application/x-www-form-urlencoded编码类型。知道通过表单上传文件时必须
		 * 指定编码类型为"multipart/form-data"。
		*/
        Headers.Add("Content-Type", "application/x-www-form-urlencoded");
    }


    public void Write(string head, string content)
    {
        if (m_index >= MAX_POST)
            return;

        m_field[m_index, HEAD] = head;
        m_field[m_index, CONTENT] = content;

        m_index++;

        if (m_content.Length == 0)
        {
            m_content += (head + "=" + content);
        }
        else
        {
            m_content += ("&" + head + "=" + content);
        }
    }

    public void EndWrite()
    {
        if (m_sum)
        {
            string hasstring = "";
            for (int i = 0; i < MAX_POST; i++)
            {
                hasstring += m_field[i, CONTENT];
            }

            hasstring += m_secretKey;

            m_content += "&key=" + Md5Sum(hasstring);
        }

        m_bytes = UTF8Encoding.UTF8.GetBytes(m_content);

    }


    // ******************************************************************************************************
    public bool BeginRead(WWW www, bool issum)
    {
        m_bytes = www.bytes;
        m_content = www.text;

        m_sum = issum;

        // 错误
        if (m_bytes == null)
        {
            m_errorRead = true;
            return false;
        }

        // 读取前2个字节,获得字符串长度
        short lenght = 0;
        this.ReadShort(ref lenght);
        if (lenght != m_bytes.Length)
        {
            m_index = lenght;
            m_errorRead = true;
            return false;
        }

        // 如果需要数字签名
        if (m_sum)
        {
            byte[] localhash = GetLocalHash(m_bytes, m_secretKey);
            byte[] hashbytes = GetCurrentHash(m_bytes);

            if (!ByteEquals(localhash, hashbytes))
            {
                m_errorRead = true;
                return false;
            }
        }

        return true;
    }

    // 忽略一个字节
    public void IgnoreByte()
    {
        if (m_errorRead)
            return;

        m_index += BYTE_LEN;
    }

    // 读取一个字节
    public void ReadByte(ref byte bts)
    {
        if (m_errorRead)
            return;

        bts = m_bytes[m_index];

        m_index += BYTE_LEN;

    }

    // 读取一个short
    public void ReadShort(ref short number)
    {
        if (m_errorRead)
            return;

        number = System.BitConverter.ToInt16(m_bytes, m_index);

        m_index += SHORT16_LEN;

    }

    // 读取一个int
    public void ReadInt(ref int number)
    {
        if (m_errorRead)
            return;

        number = System.BitConverter.ToInt32(m_bytes, m_index);

        m_index += INT32_LEN;

    }

    // 读取一个float
    public void ReadFloat(ref float number)
    {
        if (m_errorRead)
            return;

        number = System.BitConverter.ToSingle(m_bytes, m_index);

        m_index += FLOAT_LEN;

    }

    // 读取一个字符串
    public void ReadString(ref string str)
    {
        if (m_errorRead)
            return;

        short num = 0;
        ReadShort(ref num);

        str = Encoding.UTF8.GetString(m_bytes, m_index, (int)num);

        m_index += num;

    }

    // 读取一个bytes数组
    public void ReadBytes(ref byte[] byts)
    {
        if (m_errorRead)
            return;

        short len = 0;
        ReadShort(ref len);

        // 字节流
        byts = new byte[len];
        for (int i = m_index; i < m_index + len; i++)
        {
            byts[i - m_index] = m_bytes[i];
        }

        m_index += len;
    }

    // 结束读取
    public bool EndRead()
    {
        if (m_errorRead)
            return false;
        else
            return true;
    }

    // 重新计算本地数字签名
    public static byte[] GetLocalHash(byte[] bytes, string key)
    {
        // hash bytes
        byte[] hashbytes = null;

        int n = bytes.Length - HASHSIZE;
        if (n < 0)
            return hashbytes;


        // 获得key的bytes
        byte[] keybytes = System.Text.ASCIIEncoding.ASCII.GetBytes(key);


        // 创建用于hash的bytes
        byte[] getbytes = new byte[n + keybytes.Length];
        for (int i = 0; i < n; i++)
        {
            getbytes[i] = bytes[i];
        }

        keybytes.CopyTo(getbytes, n);

        System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5CryptoServiceProvider.Create();

        return md5.ComputeHash(getbytes);
    }

    // 获得当前数字签名
    public static byte[] GetCurrentHash(byte[] bytes)
    {
        byte[] hashbytes = null;
        if (bytes.Length < HASHSIZE)
        {
            return hashbytes;
        }

        hashbytes = new byte[HASHSIZE];

        for (int i = bytes.Length - HASHSIZE; i < bytes.Length; i++)
        {
            hashbytes[i - (bytes.Length - HASHSIZE)] = bytes[i];
        }
        return hashbytes;
    }

    // 比较两个bytes数组是否相等
    public static bool ByteEquals(byte[] a, byte[] b)
    {
        if (a == null || b == null)
        {
            return false;
        }

        if (a.Length != b.Length)
            return false;


        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] != b[i])
            {
                return false;
            }
        }

        return true;
    }

    // 数字签名
    public static string Md5Sum(string strToEncrypt)
    {
        byte[] bs = UTF8Encoding.UTF8.GetBytes(strToEncrypt);
        System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5CryptoServiceProvider.Create();
        byte[] hashBytes = md5.ComputeHash(bs);

        string hashString = "";
        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }

}

