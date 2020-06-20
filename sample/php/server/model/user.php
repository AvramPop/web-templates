<?php
require_once '../utils/ChromePhp.php';

class User implements JsonSerializable {
	private $id;
    private $username;
    private $password;

	public function __construct($id, $username, $password) {
		$this->id = $id;
        $this->username = $username;
        $this->password = $password;
	}

	public function getId() {
		return $this->id;
    }
    
    public function getUsername() {
		return $this->username;
    }
    
    public function getPassword() {
		return $this->password;
	}

	public function jsonSerialize() {
        $vars = get_object_vars($this);
        return $vars;
    }
}

?>
