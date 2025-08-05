using Rokid.UXR.Module;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
///
/// make 4 GameObjects to the tracked image's corners
/// make an overly plane over the tracked image
/// </summary>
public class ImageTrackedDemo : MonoBehaviour
{
    [SerializeField] private GameObject overlyPlane;

    [SerializeField] private GameObject leftUp;

    [SerializeField] private GameObject rightUp;

    [SerializeField] private GameObject leftDown;

    [SerializeField] private GameObject rightDown;

    private bool isInitialize;

    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(overlyPlane);
        Assert.IsNotNull(leftUp);
        Assert.IsNotNull(rightUp);
        Assert.IsNotNull(leftDown);
        Assert.IsNotNull(rightDown);
        if (isInitialize) return;

        //set all inactive when setup
        isInitialize = true;
        overlyPlane.SetActive(false);
        leftUp.SetActive(false);
        rightUp.SetActive(false);
        leftDown.SetActive(false);
        rightDown.SetActive(false);
    }

    public void OnTrackedImageAdded(ARTrackedImageObj imageObj)
    {
        isInitialize = true;
        leftUp.SetActive(true);
        rightUp.SetActive(true);
        leftDown.SetActive(true);
        rightDown.SetActive(true);
        overlyPlane.SetActive(true);

        //set as same when update
        OnTrackedImageUpdate(imageObj);
    }

    public void OnTrackedImageUpdate(ARTrackedImageObj imageObj)
    {
        leftUp.transform.position = imageObj.trackedImage.pose.position + imageObj.trackedImage.pose.rotation *
                                    new Vector3(-imageObj.trackedImage.bounds.extents.x,
                                        imageObj.trackedImage.bounds.extents.y, 0);
        rightUp.transform.position = imageObj.trackedImage.pose.position + imageObj.trackedImage.pose.rotation *
                                      new Vector3(imageObj.trackedImage.bounds.extents.x,
                                          imageObj.trackedImage.bounds.extents.y, 0);
        leftDown.transform.position = imageObj.trackedImage.pose.position + imageObj.trackedImage.pose.rotation *
                                      new Vector3(-imageObj.trackedImage.bounds.extents.x,
                                          -imageObj.trackedImage.bounds.extents.y, 0);
        rightDown.transform.position = imageObj.trackedImage.pose.position + imageObj.trackedImage.pose.rotation *
                                       new Vector3(imageObj.trackedImage.bounds.extents.x,
                                           -imageObj.trackedImage.bounds.extents.y, 0);

        overlyPlane.transform.localScale = new Vector3(imageObj.trackedImage.size.x, imageObj.trackedImage.size.y, 1);
        overlyPlane.transform.position = imageObj.trackedImage.pose.position;
        overlyPlane.transform.rotation = imageObj.trackedImage.pose.rotation;
    }

    public void OnTrackedImageRemoved(ARTrackedImageObj trackedImage)
    {
        isInitialize = false;
        leftUp.SetActive(false);
        rightUp.SetActive(false);
        leftDown.SetActive(false);
        rightDown.SetActive(false);
        overlyPlane.SetActive(false);
    }
}