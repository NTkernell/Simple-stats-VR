using BepInEx;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;

[BepInPlugin("com.ntkernel.simplestatsvr", "simplestatsvr", "1.0.0")]
public class SimpleTextMod : BaseUnityPlugin
{
    private GameObject textObject;
    private TextMesh textMesh;
    private float deltaTime = 0.0f;
    private bool isTextVisible = false;
    private bool pressed;
    private float gameStartTime;

    void Start()
    {
        
        gameStartTime = Time.time;
       
        PhotonNetwork.AddCallbackTarget(this);
     
        Invoke(nameof(AddTextAboveLeftHand), 3f);
    }

    void Update()
    {
       
        if (ControllerInputPoller.instance.leftControllerSecondaryButton)
        {
            if (!pressed)
            {
                isTextVisible = !isTextVisible;
                textObject.SetActive(isTextVisible);
            }

            pressed = true;
        }
        else
        {
            pressed = false;
        }

        if (textMesh != null && isTextVisible)
        {
          
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

           float fps = 1.0f / deltaTime;
            
            float frameTime = deltaTime * 1000; 
           float uptime = Time.time - gameStartTime; 

            
            float serverPing = PhotonNetwork.GetPing(); 

           
            int uptimeMinutes = Mathf.FloorToInt(uptime / 60);
            int uptimeSeconds = Mathf.FloorToInt(uptime % 60);

          textMesh.text = $"FPS: {Mathf.Ceil(fps)}\nFT: {frameTime:F2} ms\nPing: {serverPing} ms\nPlaying: {uptimeMinutes:D2}:{uptimeSeconds:D2}";
        }
    }

    void AddTextAboveLeftHand()
    {
      
        GameObject leftHand = GameObject.Find("LeftHandGorilla");  
        if (leftHand != null)
        {
            
            textObject = new GameObject("Stats");

            textMesh = textObject.AddComponent<TextMesh>();
            textMesh.fontSize = 25;
            textMesh.characterSize = 0.01f;
            textMesh.color = Color.white;

            textMesh.font = Resources.GetBuiltinResource<Font>("Arial.ttf");

            textObject.transform.SetParent(leftHand.transform);
            textObject.transform.localPosition = new Vector3(0, 0.2f, 0); 
            textObject.transform.localRotation = Quaternion.identity;

            
            textObject.SetActive(false);
        }
        else
        {
            Debug.LogError("idk uhhh beep boop :(");
        }
    }

    void OnDestroy()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
}
