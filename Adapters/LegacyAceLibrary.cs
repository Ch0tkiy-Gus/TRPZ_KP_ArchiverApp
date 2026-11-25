using System.Diagnostics;

namespace ArchiverApp.Adapters
{
    public class LegacyAceLibrary
    {
        public void PackToAce(string folder, string destinationPath)
        {
            Debug.WriteLine($"[LegacyAceLibrary] Створення ACE архіву {destinationPath} з {folder}");
        }
    }
}