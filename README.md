# 🎯 Quiz Master

**Quiz Master** is a dynamic, interactive web application built during my time at **DevCodeCamp**. It demonstrates a strong grasp of core web development concepts including asynchronous operations, API integration, DOM manipulation, and client-side logic using vanilla JavaScript.

This project provides users with a fun and challenging quiz experience using real-time questions sourced from the [OpenTriviaDB API](https://opentdb.com/).

---

## 🧠 Project Overview

- **Name**: Quiz Master  
- **Purpose**: Deliver an engaging trivia experience using real-time API data with immediate feedback and an interactive UI.

---

## 🔑 Key Features

- 🔓 **User Management**: Supports personalized quiz access through simple account creation (future improvement area)  
- 🔄 **Real-Time Quiz Data**: Questions dynamically fetched from OpenTriviaDB API  
- 🧠 **Asynchronous Operations**: Non-blocking API calls ensure smooth gameplay  
- 🎮 **Client-Side Game Logic**:  
  - Shuffles answer options  
  - Handles user input  
  - Provides real-time feedback and result display  
- 🔁 **Continuous Play**: Automatically fetches a new question after each round without reloading  
- ⚡ **DOM Manipulation**: Dynamic UI updates without page reload for a seamless user experience  

---

## 🏗️ Technologies Used

- 🧪 **JavaScript**  
- 🎨 **HTML & CSS**  
- 🌐 **[OpenTriviaDB API](https://opentdb.com/)**

---

## ✨ Interactive Flow

1. Page loads and immediately fetches a trivia question  
2. Answers are shuffled and displayed as buttons  
3. User selects an answer and receives instant feedback  
4. A new question loads automatically after a short delay  

---

## 🧩 Sample Code Snippet

```javascript
// Fetches a quiz question from OpenTriviaDB API
async function sendApiRequest() {
  const response = await fetch("https://opentdb.com/api.php?amount=1&category=9&difficulty=hard&type=multiple");
  const data = await response.json();
  quizApp(data.results[0]);
}

// Compare user’s selection with correct answer and show message
function compareAnswer(selectedAnswer, correctAnswer) {
  const isCorrect = selectedAnswer === correctAnswer;
  const message = isCorrect
    ? "Correct! YOU ARE SO SMART!"
    : `You are so Wrong! The Correct Answer is ${correctAnswer}`;
  showMessage(message, isCorrect);
}
```

---

## 💻 UI Preview

> 💡 Live UI sample:
```html
<div class="quiz-container">
  <p class="quiz-title">Project Sample - Demo Quiz</p>
  <p class="category" id="category"></p>
  <p class="difficulty" id="difficulty"></p>
  <p class="question" id="question"></p>
  <div id="resultBox" class="result-box"></div>
  <div class="button-container">
    <button class="button" id="answer1"></button>
    <button class="button" id="answer2"></button>
    <button class="button" id="answer3"></button>
    <button class="button" id="answer4"></button>
  </div>
</div>
```

---

## 🚀 Getting Started

1. **Clone the repository**

```bash
git clone https://github.com/Monyechi/QuizMaster.git
cd QuizMaster
```

2. **Open the `index.html` file** in your browser to start the app.

> No build tools, servers, or installations required – it's all vanilla JS!

---

## 🚧 Future Improvements

- Add user login & score tracking  
- Store high scores with localStorage or backend integration  
- Include more categories and difficulty levels  
- Add a progress bar and timer for increased challenge  

---

## 🔗 Links

- 🗂 [GitHub Repository](https://github.com/Monyechi/QuizMaster)
- 📚 [OpenTriviaDB API](https://opentdb.com/)

---

## 🙌 Acknowledgments

- Special thanks to [DevCodeCamp](https://devcodecamp.com/) for the foundational learning experience  
- Huge shoutout to the [OpenTriviaDB](https://opentdb.com/) team for their awesome API
