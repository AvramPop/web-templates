package org.example.web.model;

import org.example.web.domain.Asset;
import org.example.web.domain.User;

import java.sql.*;
import java.util.ArrayList;

/** Created by forest. */
public class DBConnection {
  //    private Statement stmt;

  private String dbType;
  private String dbHost;
  private String dbPort;
  private String dbName;
  private String dbUser;
  private String dbPassword;

  public DBConnection() {
    loadDBConfiguration();
  }

  private void loadDBConfiguration() {
    dbType = "postgresql";
    dbHost = "localhost";
    dbPort = "5432";
    dbName = "template";
    dbUser = "postgres";
    dbPassword = "admin";
  }

  private void loadDriver() {
    try {
      Class.forName("org.postgresql.Driver");
    } catch (ClassNotFoundException e) {
      System.err.println("Can’t load driver");
    }
  }

  private Connection dbConnection() {

    loadDriver();
    DriverManager.setLoginTimeout(60);
    try {
      String url =
          "jdbc:"
              + this.dbType
              + "://"
              + this.dbHost
              + ":"
              + this.dbPort
              + "/"
              + dbName
              + "?user="
              + this.dbUser
              + "&password="
              + this.dbPassword;
      return DriverManager.getConnection(url);
    } catch (SQLException e) {
      System.err.println("Cannot connect to the database: " + e.getMessage());
    }

    return null;
  }

  public int login(String username, String password) {
    ResultSet rs;
    User u = null;
    try (Connection connection = dbConnection()) {
      Statement stmt = connection.createStatement();
      String statement =
          "select * from users where username='" + username + "' and password='" + password + "'";
      rs = stmt.executeQuery(statement);
      if (rs.next()) {
        u = new User(rs.getInt("id"), rs.getString("username"), rs.getString("password"));
      }
      rs.close();
    } catch (SQLException e) {
      e.printStackTrace();
    }
    return u == null ? -1 : u.getId();
  }

  public ArrayList<Asset> getUserAssets(int userId) {
    ArrayList<Asset> assets = new ArrayList<>();
    ResultSet rs;
    try (Connection connection = dbConnection()) {
      Statement stmt = connection.createStatement();
      rs = stmt.executeQuery("select * from assets where userid=" + userId);
      while (rs.next()) {
        assets.add(
            new Asset(
                rs.getInt("id"),
                rs.getInt("userid"),
                rs.getString("name"),
                rs.getString("description"),
                rs.getInt("value")));
      }
      rs.close();
    } catch (SQLException e) {
      e.printStackTrace();
    }
    return assets;
  }

  public void saveAsset(int userId, String name, String description, int value) {
    try (Connection connection = dbConnection()) {
      PreparedStatement statement =
          connection.prepareStatement(
              "insert into assets(userid, \"name\", \"description\", value) values (?,?,?,?)");
      statement.setInt(1, userId);
      statement.setString(2, name);
      statement.setString(3, description);
      statement.setInt(4, value);
      statement.executeUpdate();
    } catch (SQLException e) {
      e.printStackTrace();
    }
  }
}
