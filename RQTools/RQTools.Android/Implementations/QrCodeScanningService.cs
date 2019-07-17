[assembly: Xamarin.Forms.Dependency(typeof(RQTools.Droid.Implementations.QrCodeScanningService))]
namespace RQTools.Droid.Implementations
{
    using System.Threading.Tasks;
    using RQTools.Interface;
    using ZXing.Mobile;
    using ZXing;
    using System.Collections.Generic;
    class QrCodeScanningService : IQrCodeScanningService
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
            
           
            var scanResults = await scanner.Scan(options);
            return scanResults.Text;
        }
    }
}