
namespace TaskManagement.Common;

public static class FileService
{
    private static IWebHostEnvironment _env;
    public static void Initialize(IWebHostEnvironment env) => _env = env;
    public static string BaseTasksDir
    {
        get
        {
            if(_env is null)
                throw new InvalidOperationException("WebHostEnvironment is not initialized");
            return Path.Combine(_env.WebRootPath, "Uploads", "TasksFiles");
        }
    }

    private static string _GetUniqueFileName(string Id, IFormFile formFile)
    {
        if (formFile is null || Id is null)
            return null;
        var fileExtension = Path.GetExtension(formFile.FileName);
        return $"{Id}{fileExtension}";
    }
    private static async Task _CreateFile(IFormFile formFile, string filePath)
    {
        if (filePath is not null && formFile is not null)
        {
            await using FileStream fs = File.Create(filePath);
            await formFile.CopyToAsync(fs);
        }
    }
    public static void CreateDirectory(string rootDir, string dirName)
    {
        Directory.CreateDirectory(Path.Combine(rootDir, dirName));
    }
    public static void DeleteDirectory(string rootDir, string dirName)
    {
        var dirPath = Path.Combine(rootDir, dirName);
        if (Directory.Exists(dirPath))
        {
            var dirInfo = new DirectoryInfo(dirPath);
            // delete each sub directory with its files
            foreach (var dir in dirInfo.GetDirectories())
            {
                foreach (var file in dir.GetFiles())
                    file.Delete();
                dir.Delete(true);
            }
            // delete each file in root directory
            foreach (var file in dirInfo.GetFiles())
                file.Delete();
            // finally delete the directory
            dirInfo.Delete(true);
        }
    }
    public static void DeleteDirectoryIfEmpty(string rootDir, string dirName)
    {
        var dirPath = Path.Combine(rootDir, dirName);
        if (Directory.Exists(dirPath))
        {
            var dirInfo = new DirectoryInfo(dirPath);
            if(dirInfo.GetDirectories().Length == 0 && dirInfo.GetFiles().Length == 0)
                Directory.Delete(dirPath);
        }
    }
    public static async Task<string> CreateFile(IFormFile formFile, string rootDir, string dirName, string fileName)
    {
        var uniqueFileName = _GetUniqueFileName(fileName, formFile);
        var filePath = Path.Combine(rootDir, dirName, uniqueFileName);
        await _CreateFile(formFile, filePath);
        return filePath;
    }
    public static void DeleteFile(string rootDir, string dirName, string fileName)
    {
        var filePath = Path.Combine(rootDir, dirName, fileName);
        if (File.Exists(filePath))
            File.Delete(filePath);
    }
    public static void DeleteFile(string fullFilePath)
    {
        if (File.Exists(fullFilePath))
            File.Delete(fullFilePath);
    }
}
