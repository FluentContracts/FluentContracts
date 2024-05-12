using System.IO;

namespace FluentContracts.Tests.Mocks.Data;

public class MockStream(
    bool canRead = true, 
    bool canSeek = true, 
    bool canWrite = true, 
    long length = 10, 
    long position = 5, 
    bool canTimeout = true) : Stream
{   
    public override void Flush()
    {
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        return 0;
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        return 0;
    }

    public override void SetLength(long value)
    {
        length = value;
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
    }

    public override bool CanRead => canRead;
    public override bool CanSeek => canSeek;
    public override bool CanWrite => canWrite;
    public override long Length => length;
    public override long Position
    {
        get => position;
        set => position = value;
    }

    public override bool CanTimeout => canTimeout;
}