<?php
/**
 * Created by PhpStorm.
 * User: Koobi Eyota, Jose F Salinas, Earl Blizzard
 * Date: 2/6/17
 * Time: 6:09 PM
 */
//Connection to DB
$server = "tcp:gaminggroup.database.windows.net,1433";
$connectionTimeoutSeconds = 30;
$connectionOptions = array("Database"=>"Game", "Uid"=>"salinasj14", "PWD"=>"Eastcarolina14", "LoginTimeout" => $connectionTimeoutSeconds);
$conn = sqlsrv_connect($server,$connectionOptions);
//Strings to access from client side
$name = $_GET['name'];
$kills = $_GET['kills'];
$deaths = $_GET['deaths'];
$score = $_GET['score'];
$team = $_GET['team'];
$tableName = $_GET['TBName'];
$tableOperation = $_GET['operation'];
//testing to see if DB is connected
if($conn != true)
{
    echo "did not make a connection";
}
//else
//{
//  echo "connected to my DB";
// echo "<br>";
//}
//creating the table
// call operations on site with ?operation=
if($tableOperation == 'create')
{
    echo "in create";
    echo "the name is $tableName";
    $createCmd = "CREATE TABLE $tableName
    (
	  [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
      [Name] VARCHAR(50) NOT NULL, 
      [Kills] INT NOT NULL, 
      [Deaths] INT NOT NULL, 
      [Scores] INT NOT NULL, 
      [Team] INT NOT NULL,
      [Round] INT NOT NULL
    )";
    $create = sqlsrv_query($conn, $createCmd);
}

if($tableOperation == "showData")
{
    // echo "the name is '$tableName'";
    $tsql = "SELECT * FROM $tableName ORDER BY Scores DESC";//earl
    $getProducts = sqlsrv_query($conn, $tsql);
    if ($getProducts == FALSE)
    {
        die(FormatErrors(sqlsrv_errors()));
    }
    while( $row = sqlsrv_fetch_array( $getProducts, SQLSRV_FETCH_ASSOC ))
    {
        echo $row['Name']."|".$row['Kills']."|".$row['Deaths']."|".$row['Scores']."|".$row['Team']."|".$row['Round'].";";
    }
}
//inserting values
if($tableOperation == "makePlayer")
{
    echo "it works";
    //it should auto increment and have a null value for team.
    $makeCmd = "INSERT into $tableName values ('$name',0,0,0,0,0)";
    $makePlayer = sqlsrv_query($conn, $makeCmd);
}
//removing a player from the database
if($tableOperation == "deletePlayer")
{
    echo "about to check delete player";
    echo "<br>";
    echo "you have called table operation (deletePlayer)";
    $deletePlayerCmd = "DELETE from $tableName where name = '$name'";
    $deletePlayer = sqlsrv_query($conn, $deletePlayerCmd);
    echo "you have finished calling table operation (deletePlayer) \n";
}
//update the kill in the table
if($tableOperation == "updateKill")
{
    echo "you have called table operation (updateKill)";
    $killCmd = "UPDATE $tableName set Kills = Kills+1 where Name = '$name'";
    $updateKill = sqlsrv_query($conn,$killCmd);
    echo "you have finished calling  table operation (updatingKill)";
}
//updating the death in the table
if($tableOperation == "updateDeath")
{
    echo "you have called table operation (updateDeath)";
    $deathCmd = "UPDATE $tableName set Deaths = Deaths+1 where Name = '$name'";
    $updateDeath = sqlsrv_query($conn,$deathCmd);
    echo "you have finished calling  table operation (updatingDeath)";
}
//updating the score
if($tableOperation == "updateScore")
{
    echo "you have called table operation (updateScore)";
    echo "<br>";
    $updateCmd = "UPDATE $tableName set Scores = Scores+ $score where Name = '$name'";
    $updateScore = sqlsrv_query($conn,$updateCmd);
    echo "you have finished calling table operation (updateScore)";
}
//set score
if($tableOperation == "setScore")
{
    echo "you have called table operation (setScore)";
    echo "<br>";
    $setScoreCmd = "UPDATE $tableName set Scores = $score where Name = '$name'";
    $setScore = sqlsrv_query($conn,$setScoreCmd);
    echo "you have finished calling table operation (setScore)";
}

if($tableOperation == "incRounds")
{
    echo "you have called table operation (incRounds)";
    $incRoundCmd = "UPDATE $tableName set Round = Round+1 where Scores = (Select max(Scores) From $tableName)";
    $incRounds = sqlsrv_query($conn,$incRoundCmd);
    echo "you have finished calling  table operation (incRound)";
}

//incrementing the score
if($tableOperation == "incScores")
{
    echo "you have called table operation (incScores)";
    $incCmd = "UPDATE $tableName set Scores = Scores+1 where Name = '$name'";
    $incScores = sqlsrv_query($conn,$incCmd);
    echo "you have finished calling  table operation (incScores)";
}
//setting teams
if($tableOperation == "setTeam")
{
    echo "you have called table operation (setTeam)";
    echo "team is $team \n";
    if($team == 1)
    {
        echo "you entered in setTeam 2!!!";
        $set = "UPDATE $tableName set Team = 1 where Name = '$name'";
        echo "<br>";
    }
    else if($team == 2)
    {
        echo "you entered in setTeam 2!!!";
        $set = "UPDATE $tableName set Team = 2 where Name = '$name'";
        echo "<br>";
    }
    $setTeam = sqlsrv_query($conn,$set);
    echo "you have finished calling  table operation (setTeam)";
    echo "<br>";
}

//refresh after round
if($tableOperation == "roundRefresh")
{
    echo "you have called table operation (roundRefresh)";
    $roundRefreshCmd = "UPDATE $tableName set Kills = 0 , Deaths = 0, Scores = 0 where Name = '$name'";
    $roundRefresh = sqlsrv_query($conn,$roundRefreshCmd);
    echo "you have finished calling  table operation (roundRefresh)";
}
//refresh after games
if($tableOperation == "gameRefresh")
{
    echo "you have called table operation (gameRefresh)";
    $gameRefreshCmd = "UPDATE $tableName set Kills = 0 , Deaths = 0, Scores = 0, Round = 0 where Name = '$name'";
    $gameRefresh = sqlsrv_query($conn,$gameRefreshCmd);
    echo "you have finished calling  table operation (gameRefresh)";
}
//largest round//Earl
if($tableOperation == "largestRounds")
{
    //echo "you have called largest round ";
    //echo "<br>";
    $maxRound = "SELECT Name, Round FROM $tableName WHERE Round = (Select max(Round) From $tableName)";
    $getRound = sqlsrv_query($conn, $maxRound);
    while( $row = sqlsrv_fetch_array( $getRound, SQLSRV_FETCH_ASSOC ))
    {
        echo $row['Name']."|".$row['Round']."|".";";
    }
}

//delete table
if($tableOperation == "deleteTable")
{
    echo "you have called table operation (delete) to erase $tableName";
    $deleteCmd = "Drop Table Game.dbo.$tableName";
    $delete = sqlsrv_query($conn,$deleteCmd);
    if($delete==false)
    {
        echo "There was no table to drop";
    }
    else
    {
        echo "you have finished calling table operation (delete)";
    }
}
if($tableOperation == "highestScore")
{
    $maxScore = "SELECT Name, Scores FROM $tableName WHERE Scores = (Select max(Scores) From $tableName)";
    $getScore = sqlsrv_query($conn, $maxScore);
    while( $row = sqlsrv_fetch_array( $getScore, SQLSRV_FETCH_ASSOC ))
    {
        echo $row['Name']."|".$row['Scores']."|".";";
    }
}
