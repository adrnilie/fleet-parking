using System.Reflection;

namespace FleetParking.Business;

public sealed class BusinessAssembly
{
    public static Assembly Instance => typeof(BusinessAssembly).Assembly;
}