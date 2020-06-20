window.onload = sendApiRequest

//An asynchronous function to fetch data from the API.
async function sendApiRequest() {
    let response = await fetch(`https://opentdb.com/api.php?amount=1&category=17&difficulty=hard&type=multiple`);
    console.log(response)
    let data = await response.json()
    console.log(data)
    quizApp(data)
}


Array.prototype.shuffle = function () {
    let i = this.length, j, temp;
    while (--i > 0) {
        j = Math.floor(Math.random() * (i + 1));
        temp = this[j];
        this[j] = this[i];
        this[i] = temp;
    }
    return this;
}


//function that does something with the data received from the API. The name of the function should be customized to whatever you are doing with the data
function quizApp(data) {
    let correctAnswer = data.results[0].correct_answer;
    let wrongAnswer1 = data.results[0].incorrect_answers[0];
    let wrongAnswer2 = data.results[0].incorrect_answers[1];
    let wrongAnswer3 = data.results[0].incorrect_answers[2];

    let arr = [correctAnswer, wrongAnswer1, wrongAnswer2, wrongAnswer3];
    let answers = arr.shuffle();

    document.querySelector("#category").innerHTML = `Category: ${data.results[0].category}`
    document.querySelector("#difficulty").innerHTML = `Difficulty: ${data.results[0].difficulty}`
    document.querySelector("#question").innerHTML = `Question: ${data.results[0].question}`
    document.querySelector("#answer1").innerHTML = answers[0]
    document.querySelector("#answer2").innerHTML = answers[1]
    document.querySelector("#answer3").innerHTML = answers[2]
    document.querySelector("#answer4").innerHTML = answers[3]

    const RightAnswer = answers.find(element => element === correctAnswer);
    
    let correctButton = document.querySelector("#answer1")
    console.log(correctButton)

}

let correctButton = document.querySelector("#answer1")
console.log(correctButton)

correctButton.addEventListener("click", () => {
    alert("Correct! YOU ARE SO SMART!")
    sendApiRequest()
})