using System.Text;
using Innkeep.Core.DomainModels.Authentication;
using Innkeep.Core.Interfaces.Repositories;

namespace Innkeep.Client.Data.Repositories.FileOperations;

public class AuthenticationRepository : IAuthenticationRepository
{
    private const string Folder = "Auth";

    private const string FileName = "keyfile.key";
    
    private static string FullPath => 
        Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.Split("Innkeep").First(), 
            "Innkeep", 
            Folder, 
            FileName);

    public AuthenticationInfo? Read()
    {
        if (!File.Exists(FullPath))
        {
            return null;
        }

        using var stream = File.Open(FullPath, FileMode.Open);

        using var reader = new StreamReader(stream, Encoding.UTF8);

        var token = reader.ReadLine();
        var key = reader.ReadLine();

        return new AuthenticationInfo()
        {
            PretixToken = token
        };
    }

    public void Write(AuthenticationInfo info)
    {
        using var stream = File.Open(FullPath, FileMode.Create);

        using var writer = new StreamWriter(stream);

        var token = info.PretixToken;
        var key = Guid.NewGuid().ToString().Replace("-", "");
        
        writer.WriteLine(token);
        writer.WriteLine(key);
    }
}