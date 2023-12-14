<?php

    $con = mysqli_connect("localhost","root","root","unitysql");

    //Check that connection happened
    if (mysqli_connect_errno()) {
        echo("1: Connection failed"); // error code #1 - connection failed
        printf("", mysqli_connect_error());
        exit();
    }

    $username = mysqli_real_escape_string( $con, $_POST["name"] );
    $usernameclean = htmlspecialchars($username, ENT_QUOTES, 'UTF-8'); //Every character in ascii code below 28 and special characters are avoided
    $password = $_POST["password"];

    //check if the username exists
    $namecheckquery = "SELECT username , salt, hash, score FROM players WHERE username='" . $usernameclean . "';";

    $namecheck = mysqli_query($con, $namecheckquery) or die("2: Name check query failed"); // error code #2 - name check query failed

    if(mysqli_num_rows($namecheck) != 1) {

        echo("5: This name does not exist. The number of rows with username " .$usernameclean .  "are: " . mysqli_num_rows($namecheck)); // error code #5 - number of names matching != 1
        exit();
    }

    // get login ingo from query
    $existinginfo = mysqli_fetch_assoc($namecheck);

    $salt = $existinginfo["salt"];
    $hash = $existinginfo["hash"];

    $loginhash = crypt($password, $salt);

    if($hash != $loginhash){
        echo("6: Incorrect passowrd"); // error code #6 - incorrect password
        exit();
    }

    echo("0\t" . $existinginfo["score"]);

?>