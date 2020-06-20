package org.example.web.controller;

import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.example.web.domain.Asset;
import org.example.web.model.DBConnection;
import org.json.simple.JSONArray;
import org.json.simple.JSONObject;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
@WebServlet("/assets")
public class AssetsController extends HttpServlet {
  protected void doGet(HttpServletRequest request, HttpServletResponse response)
      throws ServletException, IOException {
    String action = request.getParameter("action");
    DBConnection db = new DBConnection();

    if ((action != null) && action.equals("getAssets")) {
      int userId = Integer.parseInt(request.getParameter("userId"));
      JSONObject answer = new JSONObject();
      JSONArray jsonAssets = new JSONArray();
      for (Asset asset : db.getUserAssets(userId)) {
        JSONObject jObj = new JSONObject();
        jObj.put("id", asset.getId());
        jObj.put("userId", asset.getUserId());
        jObj.put("name", asset.getName());
        jObj.put("description", asset.getDescription());
        jObj.put("value", asset.getValue());
        jsonAssets.add(jObj);
      }
      answer.put("assets", jsonAssets);
      PrintWriter out = new PrintWriter(response.getOutputStream());
      out.println(answer);
      out.flush();
    }
  }

  protected void doPost(HttpServletRequest request, HttpServletResponse response)
      throws ServletException, IOException {
    String action = request.getParameter("action");
    DBConnection db = new DBConnection();
    if ((action != null) && action.equals("addAssets")) {
      String newAssetsToAddJSON = request.getParameter("newAssetsToAdd");
      System.out.println("received post");
      System.out.println(newAssetsToAddJSON);
      ObjectMapper mapper = new ObjectMapper();
      Asset[] assets = mapper.readValue(newAssetsToAddJSON, Asset[].class);
      for(Asset asset : assets) {
        System.out.println(asset.toString());
        db.saveAsset(asset.getUserId(), asset.getName(), asset.getDescription(), asset.getValue());
      }
    }
  }
}
