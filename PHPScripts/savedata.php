<?php

    $con = mysqli_connect("localhost","root","root","unitysql");

    //Check that connection happened
    if (mysqli_connect_errno()) {
        echo("1: Connection failed"); // error code #1 - connection failed
        printf("", mysqli_connect_error());
        exit();
    }

    $username = $_POST["name"];
    $newscore = $_POST["score"];

    //double check if the username exists
    $namecheckquery = "SELECT username FROM players WHERE username='" . $username . "';";

    $namecheck = mysqli_query($con, $namecheckquery) or die("2: Name check query failed"); // error code #2 - name check query failed

    $updatequery = "UPDATE players SET score = " . $newscore . " WHERE username =  '" . $username . "';";
    mysqli_query($con, $updatequery) or die("7: Save query failed" .  mysqli_error($con)); //error code #7 - UPDATE query failed

    echo("0");

?>