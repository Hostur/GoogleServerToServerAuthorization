using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Google.Apis.AndroidPublisher.v3;
using Google.Apis.AndroidPublisher.v3.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;

namespace GoogleServerAuthorization
{
		public class GoogleServerAuthorizationProvider
		{
			private readonly BaseClientService.Initializer _initializer;

			public GoogleServerAuthorizationProvider(string applicationName, string accountEmail, string p12CertPath, string certPassword)
			{
				X509Certificate2 x509Certificate2 = new X509Certificate2(p12CertPath, certPassword, X509KeyStorageFlags.Exportable);

				ServiceAccountCredential serviceAccountCredential = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(accountEmail)
				{
					Scopes = new[] { AndroidPublisherService.Scope.Androidpublisher }
				}.FromCertificate(x509Certificate2));

				_initializer = new BaseClientService.Initializer()
				{
					HttpClientInitializer = serviceAccountCredential,
					ApplicationName = applicationName,
				};
			}

			public async Task<bool> IsPurchaseValid(string applicationPackageName, string productName, string transactionToken)
			{
				try
				{
					using (AndroidPublisherService service = new AndroidPublisherService(_initializer))
					{
						ProductPurchase response = await service.Purchases.Products
							.Get(applicationPackageName, productName, transactionToken).ExecuteAsync()
							.ConfigureAwait(false);

						return response.PurchaseState == 0;
					}
				}
				catch (Exception e)
				{
					// [ToDo] Use some log framework.
					Console.WriteLine($"Exception occur during validating purchase {e}");
					return false;
				}
			}
		}
}
