[assembly: Xamarin.Forms.Dependency(typeof(RQTools.iOS.Implementations.QrCodeScanningService))]
namespace RQTools.iOS.Implementations
{
    using RQTools.Interface;
    using System.Threading.Tasks;
    using ZXing.Mobile;

    public class QrCodeScanningService : IQrCodeScanningService
    {
        public async Task<string> ScanAsync()
        {
            var scanner = new MobileBarcodeScanner();
            var scanResults = await scanner.Scan();
            return scanResults.Text;
        }
    }

}