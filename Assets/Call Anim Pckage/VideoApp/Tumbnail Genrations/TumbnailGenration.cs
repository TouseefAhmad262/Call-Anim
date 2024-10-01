using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TumbnailGenration : MonoBehaviour
{
    public List<string> VideoPaths;
    public List<VideoItem> Videos;
    public List<Sprite> pics;
    public GameObject VideoItemPrefeb;
    VideoPlayer player;
    public float LoadTime;
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < VideoPaths.Count; i++)
        {
            VideoItem item = Instantiate(VideoItemPrefeb, transform).gameObject.GetComponent<VideoItem>();
            item.VideoPath = VideoPaths[i];
            item.Name.text = Path.GetFileName(VideoPaths[i]);
        }
        player = GetComponent<VideoPlayer>();
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).gameObject.GetComponent<VideoItem>() != null)
            {
                Videos.Add(transform.GetChild(i).gameObject.GetComponent<VideoItem>());
            }
        }
    }

    void OnEnable()
    {
        StartCoroutine(Genrate(0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Genrate(int i)
    {
        yield return new WaitForSeconds(LoadTime/2);
        player.source = VideoSource.Url;
        player.url = Videos[i].VideoPath;
        player.time = player.clip.length / 2;
        player.Pause();
        yield return new WaitForSeconds(LoadTime/2);
        Texture2D FinalTexture = new Texture2D(player.targetTexture.width, player.targetTexture.height, TextureFormat.RGBA32, false);
        RenderTexture.active = player.targetTexture;
        FinalTexture.ReadPixels(new Rect(0, 0, player.targetTexture.width, player.targetTexture.height), 0, 0);
        FinalTexture.Apply();
        RenderTexture.active = null;
        Sprite thumbnailSprite = Sprite.Create(FinalTexture, new Rect(0, 0, FinalTexture.width, FinalTexture.height), Vector2.one * 0.5f);
        Videos[i].Thumnail.sprite = thumbnailSprite;
        pics.Add(thumbnailSprite);
        if((i + 1) < Videos.Count)
        {
            StartCoroutine(Genrate(i + 1));
        }
    }
}