using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpLoadImage : MonoBehaviour
{

    public Image TargetImage;
    public Sprite TargetSprite;
    public Image EnlargementImage;
    public HttpModel Refresh;

    public AndroidPhoto AndroidPhoto;

    public IOSPhoto IosPhoto;

    public void OnImageClick()
    {
        if (TargetImage.sprite == TargetSprite)
        {
            AndroidPhoto.SetUpLoad(this);
            IosPhoto.SetUpLoad(this);
            AndroidPhoto.OpenPhoto();
            return;
        }
        else
        {
            Enlargement();
            return;
        }
    }


    public void Enlargement()
    {
        EnlargementImage.sprite = TargetImage.sprite;
        EnlargementImage.transform.parent.gameObject.SetActive(true);
    }

    public Texture2D current2D;

    public void SendImage(Texture2D img)
    {
        AndroidPhoto.IsFuKuan = false;
        current2D = img;
        Sprite temp = Sprite.Create(img, new Rect(0, 0, 90, 90), Vector2.zero);
        TargetImage.sprite = temp;
        Up();
    }

    private void Up()
    {
        float X = 0;
        float Y = 0;
        if (current2D.width > current2D.height)
        {
            X = 1024;
            Y = ((float)(current2D.height) / (float)current2D.width) * 1024;
        }
        else
        {
            Y = 1024;
            X = ((float)(current2D.width) / (float)current2D.height) * 1024;
        }
        // Texture2D newtext = texture2DTexture(current2D, System.Convert.ToInt32(X), System.Convert.ToInt32(Y));
        Texture2D newtext = texture2DTexture(current2D, 256, 256);
        string base64String = System.Convert.ToBase64String(newtext.EncodeToJPG());
        // MessageManager._Instantiate.Show("base转换完成");
        StartCoroutine(UploadTexture(base64String));
    }

    private Texture2D texture2DTexture(Texture2D tex, int swidth, int sheght)
    {
        Texture2D res = new Texture2D(swidth, sheght, TextureFormat.ARGB32, false);
        for (int i = 0; i < res.height; i++)
        {
            for (int j = 0; j < res.width; j++)
            {
                Color newcolor = tex.GetPixelBilinear((float)j / (float)res.width, (float)i / (float)res.height);
                res.SetPixel(j, i, newcolor);
            }
        }
        res.Apply();
        return res;
    }

    IEnumerator UploadTexture(string GetTex)
    {
        //MessageManager._Instantiate.Show("上传开始");
        string url = Static.Instance.URL + "ajax_up_dkimg.php";
        WWWForm form = new WWWForm();
        form.AddField("huiyuan_id", Static.Instance.GetValue("huiyuan_id"));
        form.AddField("order_id", transform.Find("order_id").GetComponent<Text>().text);
        form.AddField("img_url", GetTex);
        DtaMD5 data = EncryptDecipherTool.UserMd5Obj();
        form.AddField("token", data.token);
        form.AddField("time", data.time);
        Debug.Log(url);
        MessageManager._Instantiate.AddLockNub();
        WWW www = new WWW(url, form);
        yield return www;
        //MSG.text = string.Empty;
        MessageManager._Instantiate.DisLockNub();
        if (www.error != null)
        {
            // MSG.text = www.error;
            MessageManager._Instantiate.Show("打款图片上传失败");
        }
        else
        {
            //  MSG.text = www.text;
            // Debug.Log(www.text);
            MessageManager._Instantiate.WindowShowMessage("打款图片上传成功");
            Invoke("RefreshView", 1f);

            //JsonData jd = JsonMapper.ToObject(www.text);
            //HttpModel my = GameManager.GetGameManager.http_body.GetTValue("Http_My");
            //my.Get();
        }
    }

    private void RefreshView()
    {
        Refresh.Get();
    }
}
