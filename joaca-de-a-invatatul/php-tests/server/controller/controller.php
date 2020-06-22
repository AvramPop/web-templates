<?php

require_once '../service/service.php';
require_once '../utils/ChromePhp.php';

class Controller
{
  private $service;

  public function __construct()
  {
    $this->service = new Service();
  }

  public function setUpEnpoints()
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
    $assets = $this->service->getAssets($userId);
    return $this->output($assets);
  }

  public function login($username, $password)
  {
    return $this->output($this->service->login($username, $password));
  }

  public function addAssets($newAssets)
  {
    $this->service->addAssets($newAssets);
  }

  public function output($param) {
    echo json_encode($param);
  }
}

$controller = new Controller();
$controller->setUpEnpoints();
