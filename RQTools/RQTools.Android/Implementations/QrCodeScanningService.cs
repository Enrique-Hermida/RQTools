[assembly: Xamarin.Forms.Dependency(typeof(RQTools.Droid.Implementations.QrCodeScanningService))]
namespace RQTools.Droid.Implementations
{
    using System.Threading.Tasks;
    using RQTools.Interface;
    using ZXing.Mobile;

    class QrCodeScanningService : IQrCodeScanningService
    {
        public async Task<string> ScanAsync()
        {
            var options = new MobileBarcodeScanningOptions();
            var scanner = new MobileBarcodeScanner();
            var scanResults = await scanner.Scan(options);
            return scanResults.Text;
        }
    }
}