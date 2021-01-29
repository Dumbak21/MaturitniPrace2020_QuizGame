using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class DataBuffer : IDisposable
{
    List<byte> _bufferList;

    byte[] _readBuffer;

    int _readPos;
    bool _bufferUpdate = false;

    public DataBuffer()
    {
        _bufferList = new List<byte>();

        _readPos = 0;
    }

    public int Posiotion()
    {
        return _readPos;
    }

    public byte[] ToArray()
    {
        return _bufferList.ToArray();
    }

    public int Count()
    {
        return _bufferList.Count;
    }

    public int Length()
    {
        return Count() - _readPos;
    }

    public void Clear()
    {
        _bufferList.Clear();
        _readPos = 0;
    }


    public void WriteBytes(byte[] input)
    {
        _bufferList.AddRange(input);
        _bufferUpdate = true;
    }
    public void WriteByte(byte input)
    {
        _bufferList.Add(input);
        _bufferUpdate = true;
    }
    public void WriteInt(int input)
    {
        _bufferList.AddRange(BitConverter.GetBytes(input));
        _bufferUpdate = true;
    }
    public void WriteFloat(float input)
    {
        _bufferList.AddRange(BitConverter.GetBytes(input));
        _bufferUpdate = true;
    }
    public void WriteString(string input)
    {
        _bufferList.AddRange(BitConverter.GetBytes(input.Length));
        _bufferList.AddRange(Encoding.ASCII.GetBytes(input));
        _bufferUpdate = true;
    }



    public int ReadInt(bool peek = true)
    {
        if (_bufferList.Count > _readPos)
        {
            if (_bufferUpdate)
            {
                _readBuffer = _bufferList.ToArray();
                _bufferUpdate = false;
            }
            int value = BitConverter.ToInt32(_readBuffer, _readPos);
            if (peek & _bufferList.Count > _readPos)
            {
                _readPos += 4;
            }
            return value;
        }
        else
        {
            throw new Exception("Buffer full");
        }
    }
    public float ReadFloat(bool peek = true)
    {
        if (_bufferList.Count > _readPos)
        {
            if (_bufferUpdate)
            {
                _readBuffer = _bufferList.ToArray();
                _bufferUpdate = false;
            }
            float value = BitConverter.ToSingle(_readBuffer, _readPos);
            if (peek & _bufferList.Count > _readPos)
            {
                _readPos += 4;
            }
            return value;
        }
        else
        {
            throw new Exception("Buffer full");
        }
    }
    public byte ReadByte(bool peek = true)
    {
        if (_bufferList.Count > _readPos)
        {
            if (_bufferUpdate)
            {
                _readBuffer = _bufferList.ToArray();
                _bufferUpdate = false;
            }
            byte value = _readBuffer[_readPos];
            if (peek & _bufferList.Count > _readPos)
            {
                _readPos += 1;
            }
            return value;
        }
        else
        {
            throw new Exception("Buffer full");
        }
    }
    public byte[] ReadBytes(int length, bool peek = true)
    {
        if (_bufferUpdate)
        {
            _readBuffer = _bufferList.ToArray();
            _bufferUpdate = false;
        }
        byte[] value = _bufferList.GetRange(_readPos, length).ToArray();
        if (peek & _bufferList.Count > _readPos)
        {
            _readPos += 1;
        }
        return value;
    }
    public string ReadString(bool peek = true)
    {
        int length = ReadInt();
        if (_bufferUpdate)
        {
            _readBuffer = _bufferList.ToArray();
            _bufferUpdate = false;
        }
        string value = Encoding.ASCII.GetString(_readBuffer, _readPos, length);
        if (peek & _bufferList.Count > _readPos)
        {
            _readPos += length;
        }
        return value;
    }


    private bool disposed = false;
    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                _bufferList.Clear();
            }
            _readPos = 0;

        }
        disposed = true;
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

}
