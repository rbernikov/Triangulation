namespace Triangulation.Controllers
{
    public interface IMainController
    {
        void OnMapLoad(string fileName);
        void OnLoad(string fileName);
        void OnSaveZones(string fileName);
        void OnWatershedExtract();
        void OnBoundaryExtract();
        void OnZoneUnion(int method, float percent);
    }
}