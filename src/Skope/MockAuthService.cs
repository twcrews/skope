namespace Skope;

public class MockAuthService
{
    public bool IsAuthenticated { get; private set; }
    public event Action? OnChange;

    public void SignIn()
    {
        IsAuthenticated = true;
        OnChange?.Invoke();
    }

    public void SignOut()
    {
        IsAuthenticated = false;
        OnChange?.Invoke();
    }
}
