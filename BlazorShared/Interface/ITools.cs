﻿namespace BlazorShared.Services
{
    public interface ITools
    {
        Task<string> CheckPermissionsCamera();
        Task<string> TakePhoto();

        Task<string> CheckPermissionsLocation();
        Task<string> GetCachedLocation();

        Task<string> GetCurrentLocation();

        Task<string> CheckMock();

        double DistanceBetweenTwoLocations();

        void ShowSettingsUI();
        string GetAppInfo();
        Task<string> NavigateToBuilding25();
        Task<string> NavigateToBuilding();
        Task<string> NavigateToBuildingByPlacemark();
        Task<string> DriveToBuilding25();
        Task<string> TakeScreenshotAsync();
    }

    public class NullService : ITools
    {
        public Task<string> CheckPermissionsCamera() => Task.FromResult("未实现");
        public Task<string> CheckPermissionsLocation() => Task.FromResult("未实现");
        public Task<string> CheckMock() => Task.FromResult("未实现");

        public double DistanceBetweenTwoLocations() => 0;

        public Task<string> GetCachedLocation() => Task.FromResult("未实现");

        public Task<string> GetCurrentLocation() => Task.FromResult("未实现");
        public Task<string> TakePhoto() => Task.FromResult("未实现");
        public void ShowSettingsUI() { }
        public string GetAppInfo() => "未实现";
        public Task<string> NavigateToBuilding25() => Task.FromResult("未实现");
        public Task<string> NavigateToBuilding() => Task.FromResult("未实现");
        public Task<string> NavigateToBuildingByPlacemark() => Task.FromResult("未实现");
        public Task<string> DriveToBuilding25() => Task.FromResult("未实现");
        public Task<string> TakeScreenshotAsync() => Task.FromResult("未实现");

    }
}
