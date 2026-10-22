using loader_5_0_lib.Classes;

namespace loader_5_0_lib.Service.Interfaces
{
    public interface ILoader
    {
        public void StartUploading();
        public ActiveUpload? activeUpload { get; }
    }
}
