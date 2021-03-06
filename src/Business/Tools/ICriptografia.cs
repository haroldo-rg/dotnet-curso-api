namespace Curso.Api.Business.Tools
{
    public interface ICriptografia
    {
        string Decrypt(string texto);
        string Encrypt(string texto);
    }
}