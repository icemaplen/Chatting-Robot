using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using LitJson;
using System;
using UnityEngine.UI;

public enum EMessageType { Self, Others }

public class Chatting : MonoBehaviour
{
    public InputField inputField;
    public Text resultText;

    public GameObject leftItem;
    public GameObject rightItem;

    public Transform scrollRectTrans;

    private readonly string url = "http://www.tuling123.com/openapi/api";
    private string message;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            OnSendButtonDown();
        }
    }

    public void OnSendButtonDown()
    {
        CreateMsgItem(EMessageType.Self);
        Invoke("SendMessage", 1);
        message = inputField.text;
        inputField.text = string.Empty;
    }

    private void CreateMsgItem(EMessageType msgType, string msg = null)
    {
        GameObject go;
        switch (msgType)
        {
            case EMessageType.Self:
                go = Instantiate(rightItem);
                go.transform.Find("TextBg/Text").GetComponent<Text>().text = inputField.text;
                break;

            case EMessageType.Others:
                go = Instantiate(leftItem);
                go.transform.Find("TextBg/Text").GetComponent<Text>().text = msg;
                break;
            default:
                go = null;
                break;
        }

        float height = go.GetComponent<MessageItem>().Height;
        go.transform.SetParent(scrollRectTrans);
        scrollRectTrans.GetComponent<Content>().AddHeight(height);
    }

    private void SendMessage()
    {
        string request = message;
        RequstData requstData = new RequstData(request, "1");
        string result = PostWebRequest(url, requstData.ConverToJson());
        JsonData dejson = JsonMapper.ToObject(result);
        OnReceiveMsg(dejson["text"].ToString());
    }

    private void OnReceiveMsg(string msg)
    {
        CreateMsgItem(EMessageType.Others, msg);
    }

    /// <summary>
    /// Post提交数据
    /// </summary>
    /// <param name="postUrl">URL</param>
    /// <param name="paramData">参数</param>
    /// <returns></returns>
    private string PostWebRequest(string postUrl, string paramData)
    {
        string ret = string.Empty;
        try
        {
            if (!postUrl.StartsWith("http://"))
                return "";

            byte[] byteArray = Encoding.UTF8.GetBytes(paramData); //转化
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
            webReq.Method = "POST";
            //webReq.ContentType = "application/x-www-form-urlencoded";

            webReq.ContentLength = byteArray.Length;
            Stream newStream = webReq.GetRequestStream();
            newStream.Write(byteArray, 0, byteArray.Length);//写入参数
            newStream.Close();
            HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            ret = sr.ReadToEnd();
            sr.Close();
            response.Close();
            newStream.Close();
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        return ret;
    }
}

