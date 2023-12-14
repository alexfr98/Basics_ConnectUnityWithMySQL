<?php

    $con = mysqli_connect("localhost","root","root","unitysql");

    //Check that connection happened
    if (mysqli_connect_errno()) {
        echo("1: Connection failed"); // error code #1 - connection failed
        printf("", mysqli_connect_error());
        exit();
    }

    $username = $_POST["name"];
    $password = $_POST["password"];
    
    //check if the username exists
    $namecheckquery = "SELECT username FROM players WHERE username='" . $username . "';";

    $namecheck = mysqli_query($con, $namecheckquery) or die("2: Name check query failed"); // error code #2 - name check query failed

    if(mysqli_num_rows($namecheck) > 0) {

        echo("3: Name already exists"); // error code #3 - name exists cannot register
        exit();
    }

    // add user to the table
    // convert the passsword to hash and salt to secure the password (hashing and salting) --> we dont want to save the password as plaint ext in the database
    // the hashing means that they cant put in some information amd get their password
    // the salting means they cant use look-up tables which are ways that hackers and other bad actors will use to quickly break through encrypted passwords and figure out what they are

    //encryption --> sha-256 encryption. The first 5 indicates the type of incryption and the 5000 the number of rounds. Run through 500 rounds shifting these characters. 

    $_salt = "\$5\$rounds=5000\$" . "steamedhams" . $username . "\$"; //steamedhams is just a random goofy word to use it in the encryption. The username will be used too. WE DONT WANT TO USE THE PASSWORD
    
    $_hash = crypt($password, $_salt); //--> this is not the best way to secure things. If we want to secure it better, just check more information

    $insertuserquery = "INSERT INTO players (username, hash, salt) VALUES ('". $username . "', '" . $_hash . "', '" . $_salt . "');";
    mysqli_query($con, $insertuserquery) or die("4: Insert player query failed: ". mysqli_error($con)); // error code #4 - insert query failed

    echo("0"); // SUCCESS


?>