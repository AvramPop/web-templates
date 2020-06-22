<?php


require_once '../utils/ChromePhp.php';

class DBUtils {
	private $host = '127.0.0.1';
	private $db   = 'template';
	private $user = 'root';
	private $pass = '';
	private $charset = 'utf8';

	private $pdo;
	private $error;

	public function __construct () {
		$dsn = "mysql:host=$this->host;dbname=$this->db;charset=$this->charset";
		$opt = array(PDO::ATTR_ERRMODE            => PDO::ERRMODE_EXCEPTION,
			PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC,
			PDO::ATTR_EMULATE_PREPARES   => false);
		try {
			$this->pdo = new PDO($dsn, $this->user, $this->pass, $opt);
		} // Catch any errors
		catch(PDOException $e){
			$this->error = $e->getMessage();
			echo "Error connecting to DB: " . $this->error;
		}
	}

	public function login($username, $password){
		$stmt = $this->pdo->query("select * from users where username='" . $username . "' and password = '" . $password . "'");
        return $stmt->fetchAll(PDO::FETCH_ASSOC);
	}

	public function getAllAssetsOfUser($userId){
		$stmt = $this->pdo->query("select * from assets where userid = " . $userId);
        return $stmt->fetchAll(PDO::FETCH_ASSOC);
	}

	public function saveAsset($userId, $name, $description, $value){
		$affected_rows = $this->pdo->exec("insert into assets(`userid`, `name`, `description`, `value`) values(" . $userId . ", '" . $name . "', '" . $description ."', ". $value .")");
		return $affected_rows;
	}
}
