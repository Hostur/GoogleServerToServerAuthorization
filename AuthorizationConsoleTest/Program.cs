using System;
using System.IO;
using System.Threading.Tasks;
using GoogleServerAuthorization;

namespace AuthorizationConsoleTest
{
		class Program
		{
			private const string APPLICATION_NAME = "MyAppName";
			private const string ACCOUNT_EMAIL = "MyGoogleServiceAccountEmail";
			private const string CERT_PASSWORD = "notasecret";
			private static string CERT_NAME = "mycert.p12";

			private const string APP_PACKAGE_NAME = "myGamePackageName";
			private const string MY_INAPP_PRODUCT_NAME = "my.product.name";
			private const string MY_FAKE_TRANSACTION_ID = "dsadsa-dasdasdas-asdsadsa-dasdsadsa-dsadsadsa";

			private static string GetCertPath => Path.Combine(Directory.GetCurrentDirectory(), CERT_NAME);

			private static GoogleServerAuthorizationProvider _provider;
				static async Task Main(string[] args)
				{
					Console.WriteLine("Starting authorization test...");
					_provider = new GoogleServerAuthorizationProvider(APPLICATION_NAME, ACCOUNT_EMAIL, GetCertPath, CERT_PASSWORD);

					bool isPurchaseValid = await _provider.IsPurchaseValid(APP_PACKAGE_NAME, MY_INAPP_PRODUCT_NAME, MY_FAKE_TRANSACTION_ID).ConfigureAwait(false);

					Console.WriteLine($"Purchase validation result: {isPurchaseValid}");
					Console.ReadLine();
				}
		}
}
