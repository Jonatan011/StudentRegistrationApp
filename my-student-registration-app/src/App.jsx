import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import StudentList from './Components/StudentList/StudentList.jsx';
import StudentCourses from './Components/RegisterCourses/RegisterCourses.jsx';
import CreateStudent from './Components/RegisterStudent/RegisterStudent.jsx';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<StudentList />} />
        <Route path="/students/new" element={<CreateStudent />} />
        <Route path="/students/:studentId/courses" element={<StudentCourses />} />
      </Routes>
    </Router>
  );
}

export default App;
