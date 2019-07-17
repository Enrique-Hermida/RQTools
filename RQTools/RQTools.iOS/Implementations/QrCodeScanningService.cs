[assembly: Xamarin.Forms.Dependency(typeof(RQTools.iOS.Implementations.QrCodeScanningService))]
namespace RQTools.iOS.Implementations
{
    using RQTools.Interface;
    using System.Threading.Tasks;
    using ZXing.Mobile;
    using ZXing;
    using System.Collections.Generic;

    public class QrCodeScanningService : IQrCodeScanningService
    {
        public async Task<string> ScanAsync()
        {
            
            var options = new MobileBarcodeScanningOptions();
            var scanner = new MobileBarcodeScanner();
            scanner.TopText = "El escaneo es automático";
            scanner.BottomText = "Solo alinea la línea roja con el código";
            options.AutoRotate = true;
            options.TryHarder = true;
            options.PossibleFormats = new List<BarcodeFormat> {
                BarcodeFormat.QR_CODE,
                BarcodeFormat.CODE_128,
                BarcodeFormat.EAN_13
            };
            var scanResults = await scanner.Scan();
            return scanResults.Text;
        }
    }

}