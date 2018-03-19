using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;


class RequstData
{
    private const string key = "b803a8afffcc02839d264ff55f0a1630";

    public string Key { get; private set; }
    public string Info { get; set; }
    public string UserID { get; set; }


    public RequstData(string info, string userid)
    {
        Info = info;
        UserID = userid;
    }


    public string ConverToJson()
    {
        JsonData jd = new JsonData();
        jd["key"] = key;
        jd["info"] = Info;
        jd["userid"] = UserID;

        return JsonMapper.ToJson(jd);
    }
}

