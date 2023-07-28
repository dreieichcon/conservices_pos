using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Innkeep.Core.Validation;

public static class ServerValidator
{
	public static bool ValidateServerCertificate(object sender,X509Certificate? certificate,X509Chain? chain,SslPolicyErrors sslPolicyErrors)
	{
		return sslPolicyErrors is SslPolicyErrors.RemoteCertificateNameMismatch or SslPolicyErrors.None;

		// ask to trust here?
	}
}