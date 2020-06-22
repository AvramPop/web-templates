<?php

class Asset implements JsonSerializable {
	private $id;
    private $userId;
    private $name;
    private $description;
    private $value;


	public function __construct($id, $userId, $name, $description, $value) {
		$this->id = $id;
        $this->userId = $userId;
        $this->name = $name;
        $this->description = $description;
        $this->value = $value;
	}

	// public function getId() {
	// 	return $this->id;
    // }

	public function jsonSerialize() {
        $vars = get_object_vars($this);
        return $vars;
    }
}

?>
