namespace Monno.SharedKernel.Services;

public abstract class BaseService : IDisposable
{
    private void ReleaseUnmanagedResources()
    {
        // TODO release unmanaged resources here
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~BaseService()
    {
        ReleaseUnmanagedResources();
    }
}