<?php

require_once '../model/model.php';
require_once '../view/view.php';
require_once '../utils/ChromePhp.php';

class Controller
{
  private $view;
  private $model;

  public function __construct()
  {
    $this->model = new Model();
    $this->view = new View();
  }

  public function service()
  {

    if (isset($_GET['action']) && !empty($_GET['action'])) {

      if ($_GET['action'] == "getAssets") {
        $this->{$_GET['action']}($_GET['userId']);
      }
    }

    if (isset($_POST['action']) && !empty($_POST['action'])) {
      if ($_POST['action'] == "login") {
        $this->{$_POST['action']}($_POST['user'], $_POST['password']);
      } else if ($_POST['action'] == "addAssets") {
        $this->{$_POST['action']}($_POST['newAssetsToAdd']);
      }
    }
  }

  public function getAssets($userId)
  {
    $assets = $this->model->getAssets($userId);
    return $this->view->output($assets);
  }

  public function login($username, $password)
  {
    return $this->view->output($this->model->login($username, $password));
  }

  public function addAssets($newAssets)
  {
    $this->model->addAssets($newAssets);
  }
}

$controller = new Controller();
$controller->service();
