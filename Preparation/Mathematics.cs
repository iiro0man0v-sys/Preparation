using System.Security.Cryptography.X509Certificates;
namespace Preparation;
static class Mathematics
{
    public static int IncreaseByOne(int x)
    {
        x+=1;
        return x;
    }

    public static void IncreaseByOneRef(ref int x)
    {
        x+=1;
    }

    public static void DivideByTwo(int x, out int quotient, out int remainder)
    {
        quotient = x/2;
        remainder = -(quotient*2) +x;
    }
}