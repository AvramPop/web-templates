<?php

require_once '../repo/DBUtils.php';
require_once '../model/asset.php';
require_once '../utils/ChromePhp.php';


class Service
{
	private $db;

	public function __construct()
	{
		$this->db = new DBUtils();
	}

	public function addAssets($newAssets)
	{
		$temp = json_decode($newAssets);
		foreach ($temp as $key => $value) {
			$this->db->saveAsset($value->userId, $value->name, $value->description, $value->value);
		}
	}

	private function inArray($value, $list)
	{
		foreach ($list as $item) {
			if ($item['id'] == $value['id']) return true;
		}
		return false;
	}


	public function getAssets($userId)
	{
		$resultset = $this->db->getAllAssetsOfUser($userId);
		$assets = array();
		foreach ($resultset as $key => $value) {
			array_push($assets, new Asset($value["id"], $value["userid"], $value["name"], $value["description"], $value["value"]));
		}
		return ["assets" => $assets];
	}

	public function login($username, $password)
	{
		$users = $this->db->login($username, $password);
		if (count($users) == 1) {
			return ["success" => true, "userId" => $users[0]["id"]];
		} else {
			return ["success" => false];
		}
	}
}
