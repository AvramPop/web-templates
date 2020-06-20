<?php

require_once '../repo/DBUtils.php';
require_once 'asset.php';
require_once 'user.php';
require_once '../utils/ChromePhp.php';


class Model {
	private $db;

	public function __construct() {
		$this->db = new DBUtils ();
	}
	public function addAssets($newAssets){
		$temp = json_decode($newAssets);
		foreach ($temp as $key => $value) {
			$this->db->saveAsset($value->userId, $value->name, $value->description, $value->value);
		}
	}

	public function inArray($value, $list){
		foreach ($list as $item) {
			if($item['id'] == $value['id']) return true;
		}
		return false;
	}


	public function getAssets($userId){
		$resultset = $this->db->getAllAssetsOfUser($userId);
		$assets = array();
		foreach ($resultset as $key => $value) {
			array_push($assets, $value);
		}
		return ["assets" => $assets];
	}

	public function login($username, $password){
		$resultset = $this->db->login($username, $password);
		$users = array();
		foreach ($resultset as $key => $value) {
			array_push($users, $value);
		}

		if (count($users) == 1){
			ChromePhp::log($users[0]["id"]);
			return ["success" => true, "userId" => $users[0]["id"]];
		} else {
			return ["success" => false];
		}
		
	}

}
