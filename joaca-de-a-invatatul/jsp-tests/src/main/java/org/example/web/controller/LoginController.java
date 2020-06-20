package org.example.web.controller;

import com.fasterxml.jackson.databind.ObjectMapper;
import lombok.*;
import org.example.web.model.DBConnection;
import org.json.simple.JSONObject;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.Arrays;
import java.util.HashMap;

@WebServlet("/login")
public class LoginController extends HttpServlet {

  public LoginController() {
    super();
  }

  protected void doGet(HttpServletRequest request, HttpServletResponse response)
      throws ServletException, IOException {}

  protected void doPost(HttpServletRequest request, HttpServletResponse response)
      throws ServletException, IOException {
    String action = request.getParameter("action");
    DBConnection db = new DBConnection();
    if ((action != null) && action.equals("login")) {
      String username = request.getParameter("user");
      String password = request.getParameter("password");
      JSONObject answer = new JSONObject();
      int userId = db.login(username, password);
      if (userId != -1) {
        answer.put("success", true);
        answer.put("userId", userId);
      } else {
        answer.put("success", false);
      }

    } else if (action.equals("test")) {
      JSONObject elem1 = new JSONObject();
      elem1.put("key1", "value1");
      JSONObject elem2 = new JSONObject();
      elem2.put("key2", "value2");
      JSONObject answer = new JSONObject();
      answer.put("array", Arrays.asList(elem1, elem2));
      PrintWriter out = new PrintWriter(response.getOutputStream());
      out.println(answer);
      out.flush();
    }
  }

}
