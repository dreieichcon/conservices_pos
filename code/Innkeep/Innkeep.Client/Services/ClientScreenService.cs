using Innkeep.Services.Client.Interfaces.Internal;

namespace Innkeep.Client.Services;

public class ClientScreenService : IClientScreenService
{
    public async Task ShowClientWindow()
    {
        var window = new ClientWindow();
        window.Show();
    }
}