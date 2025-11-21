namespace AReyes.Services.Interfaces
{
    public interface IImagenService
    {
        Task<string> GuardarImagenAsync(IFormFile file);
    }
}
