<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="CineProducto.Callbacks.List" %>
<%
    Dictionary<string, string> search = new Dictionary<string,string>();
    foreach (string skey in Request.Form.Keys)
    {
        if (skey == "project_id" ||
            skey == "project_name" ||
            skey == "project_request_date" ||
            skey == "project_clarification_request_date" ||
            skey == "project_clarification_response_date" ||
            skey == "state_name" ||
            skey == "project_resolution_date"||
            skey == "resolution_path")
            
        {
            search.Add(skey, Request.Form[skey]);
        }
    }
    List<List<string>> rows = dataSet(int.Parse(Request["page"]), int.Parse(Request["rows"]), Request["sidx"], Request["sord"], search);
    Response.Write("<rows>");
    
Response.Write("<page>" + Request["page"] + "</page>");
Response.Write("<total>" + Math.Ceiling((double)countTotal/int.Parse(Request["rows"])) + "</total>");
Response.Write("<records>" + countTotal + "</records>");
    foreach (List<string> set in rows){
        Response.Write("<row id ='" + set[0] + "'>");
        foreach (string data in set){
            Response.Write("<cell>" + data + "</cell>");
        }
        Response.Write("</row>");
    }
    Response.Write("</rows>");
     %>