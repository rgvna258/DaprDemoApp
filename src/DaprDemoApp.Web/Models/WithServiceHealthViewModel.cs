namespace DaprDemoApp.Web.Models;

public class WithServiceHealthViewModel<T>
{
    public WithServiceHealthViewModel(bool isDependentServiceHealthy = true, T? value = default)
    {
        IsDependentServiceHealthy = isDependentServiceHealthy;
        Value = value;
    }

    public bool IsDependentServiceHealthy { get; init; }

    public T? Value { get; init; }
}
