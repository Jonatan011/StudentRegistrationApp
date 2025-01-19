import { useState, useEffect } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import './StudentList.css';

const StudentList = () => {
  const [students, setStudents] = useState([]);
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchStudents = async () => {
      try {
        const response = await axios.get('http://localhost:5275/api/Students');
        setStudents(response.data);
        setError(null);
      } catch (error) {
        console.error('Error fetching students:', error);
        setError('No se pudo cargar la lista de estudiantes. Inténtalo de nuevo más tarde.');
      }
    };

    fetchStudents();
  }, []);

  const navigateToCourses = (studentId) => {
    navigate(`/students/${studentId}/courses`);

  };
  

  const navigateToCreateStudent = () => {
    navigate('/students/new');
  };

  return (
    <div className="main-wrapper">
      <div className="container">
        <h1 className="title">Listado de Estudiantes</h1>
        {error && <p className="error">{error}</p>}
        <ul className="list">
          {students.map((student) => (
            <li key={student.id} className="list-item">
              <span>{student.fullName}</span>
              <button
                className="view-button"
                onClick={() => navigateToCourses(student.id)}
              >
                Ver Cursos
              </button>
            </li>
          ))}
        </ul>
        <button className="create-button" onClick={navigateToCreateStudent}>
          Crear Nuevo Estudiante
        </button>
      </div>
    </div>
  );

};
export default StudentList;
