/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Play.AssetDelivery;
using UnityEngine.UI;
using TMPro;

public class RetrieveAssetBundles : MonoBehaviour
{
    // Start is called before the first frame update
    public FirstLoad firstLoad;
    [SerializeField] private Slider progressSlider;
    [SerializeField] private Button useMobileNetwork;
    [SerializeField] private TMP_Text downloadInfo;
    [SerializeField] private TMP_Text downloadPercentage;



    void Start()
    {
        PlayAssetPackRequest bundleRequest = PlayAssetDelivery.RetrieveAssetPackAsync("",true);

        // Loads the AssetBundle from disk, downloading the asset pack containing it if necessary.


    }

    private IEnumerator LoadAssetBundleCoroutine(string assetBundleName)
    {

        PlayAssetBundleRequest bundleRequest =
            PlayAssetDelivery.RetrieveAssetBundleAsync(assetBundleName);

        while (!bundleRequest.IsDone)
        {
            if (bundleRequest.Status == AssetDeliveryStatus.WaitingForWifi)
            {
                var userConfirmationOperation = PlayAssetDelivery.ShowCellularDataConfirmation();

                // Wait for confirmation dialog action.
                yield return userConfirmationOperation;

                if ((userConfirmationOperation.Error != AssetDeliveryErrorCode.NoError) ||
                   (userConfirmationOperation.GetResult() != ConfirmationDialogResult.Accepted))
                {
                    // The user did not accept the confirmation - handle as needed.
                }

                // Wait for Wi-Fi connection OR confirmation dialog acceptance before moving on.
                yield return new WaitUntil(() => bundleRequest.Status != AssetDeliveryStatus.WaitingForWifi);
            }

            // Use bundleRequest.DownloadProgress to track download progress.
            // Use bundleRequest.Status to track the status of request.

            yield return null;
        }

        if (bundleRequest.Error != AssetDeliveryErrorCode.NoError)
        {
            // There was an error retrieving the bundle. For error codes NetworkError
            // and InsufficientStorage, you may prompt the user to check their
            // connection settings or check their storage space, respectively, then
            // try again.
            yield return null;
        }

        // Request was successful. Retrieve AssetBundle from request.AssetBundle.
        AssetBundle assetBundle = bundleRequest.AssetBundle;
    }

    private IEnumerator LoadAsynchronously(int scene)
    {
        yield return new WaitForSeconds(1f);
        PlayAssetBundleRequest bundleRequest = PlayAssetDelivery.RetrieveAssetBundleAsync("");

        float progress = bundleRequest.DownloadProgress;

        // Returns true if:
        //   * it had either completed the download, installing, and loading of the AssetBundle,
        //   * OR if it has encountered an error.

        bool done = bundleRequest.IsDone;

        while(done)
        {
            done = bundleRequest.IsDone;
            progress = bundleRequest.DownloadProgress;
            // Returns status of retrieval request.
            AssetDeliveryStatus status = bundleRequest.Status;

            progressSlider.value = progress;

            switch (status)
            {
                case AssetDeliveryStatus.Pending:
                // Asset pack download is pending - N/A for install-time assets.
                case AssetDeliveryStatus.Retrieving:
                // Asset pack is being downloaded and transferred to app storage.
                // N/A for install-time assets.
                case AssetDeliveryStatus.Available:
                // Asset pack is downloaded on disk but NOT loaded into memory.
                // For PlayAssetPackRequest(), this indicates that the request is complete.
                case AssetDeliveryStatus.Loading:
                // Asset pack is being loaded.
                case AssetDeliveryStatus.Loaded:
                // Asset pack has finished loading, assets can now be loaded.
                // For PlayAssetBundleRequest(), this indicates that the request is complete.
                case AssetDeliveryStatus.Failed:
                // Asset pack retrieval has failed.
                case AssetDeliveryStatus.WaitingForWifi:
                // Asset pack retrieval paused until either the device connects via Wi-Fi,
                // or the user accepts the PlayAssetDelivery.ShowCellularDataConfirmation dialog.
                default:
                    break;
            }
        }

        firstLoad.LoadScene(scene);
    }


}
*/