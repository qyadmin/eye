using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class AndroidPhoto : MonoBehaviour
{

    public RawImage IMG;
    public IOSPhoto ios;

    [HideInInspector]
    public bool IsFuKuan;


    private UpLoadImage _fuKuanLoad;

    public Text debug;
    //打开相册	
    public void OpenPhoto()
    {
#if UNITY_EDITOR

        test();
#endif
        //debug.text=debug.text+"*";
#if UNITY_ANDROID
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("OpenGallery");
#elif UNITY_IPHONE
		ios.OpenPhoto();
#endif

    }

    //打开相机
    public void OpenCamera()
    {
#if UNITY_ANDROID
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("takephoto");
#elif UNITY_IPHONE
		ios.OpenCamera();
#endif
    }

    public void SetUpLoad(UpLoadImage temp)
    {
        IsFuKuan = true;
        _fuKuanLoad = temp;
    }

    public void GetImagePath(string imagePath)
    {
        if (imagePath == null)
            return;
        StartCoroutine("Load", imagePath);
    }

    public void GetTakeImagePath(string imagePath)
    {
        if (imagePath == null)
            return;
        StartCoroutine("Load", imagePath);
    }
    [SerializeField]
    Texture2D texture;
    public void test()
    {
        _fuKuanLoad.SendImage(texture);
        //LoadImage.GetLoadIamge.SendImage(texture);
    }

    private IEnumerator Load(string imagePath)
    {
        MessageManager._Instantiate.AddLockNub();
        WWW www = new WWW("file://" + imagePath);
        yield return www;
        MessageManager._Instantiate.DisLockNub();
        if (www.error == null)
        {
            //成功读取图片，写自己的逻辑
            //GetComponent<ChangePhoto>().LoadAndroidImageOK(www.texture);

            //MessageManager._Instantiate.Show("现车获取成功，等待上传");

            if (IsFuKuan)
            {
                _fuKuanLoad.SendImage(www.texture);
            }
            else
            {
                IMG.texture = www.texture;
                LoadImage.GetLoadIamge.SendImage(www.texture);
            }


        }
        else
        {
            Debug.LogError("LoadImage>>>www.error:" + www.error);
        }
    }
}
