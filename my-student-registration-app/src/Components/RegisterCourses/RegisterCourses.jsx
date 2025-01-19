import { useState, useEffect } from 'react';
import axios from 'axios';
import './RegisterCourses.css';
import { useNavigate } from 'react-router-dom';
import { useParams } from 'react-router-dom';

const RegisterCourses = () => {
  const { studentId } = useParams(); 

  const [courses, setCourses] = useState([]);
  const [selectedCourses, setSelectedCourses] = useState([]);
  const [errorMessage, setErrorMessage] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
    const fetchCourses = async () => {
      try {
        const response = await axios.get('http://localhost:5275/api/courses');
        setCourses(response.data);
        const selectedCoursesResponse = await axios.get(`http://localhost:5275/api/Students/${studentId}`);
        setSelectedCourses(selectedCoursesResponse.data.courseIds);
      } catch (error) {
        console.error('Error fetching courses:', error);
      }
    };

    fetchCourses();
  }, [studentId]);

  const handleCourseSelection = async (courseId) => {
    try {
      await axios.post('http://localhost:5275/api/Students/register-courses', { 
        studentId: studentId, 
        courseIds: [courseId] ,
      });
      setSelectedCourses(prev => [...prev, courseId]);
    } catch (error) {
      if (error.response && error.response.status) {
        setErrorMessage(error.response.data.message);
      } else {
        console.error('Error al registrar curso:', error);
      }
    }
  };

  const onBack= () =>{
    navigate(`/`);
  };

  return (
    <div className="form-container">
      <h2 className="form-title">Registrar Cursos para Estudiante</h2>
      <table className="course-table">
        <thead>
          <tr>
            <th>Curso</th>
            <th>Profesor</th>
            <th>Acción</th>
          </tr>
        </thead>
        <tbody>
          {courses.map(course => (
            <tr key={course.id}>
              <td>{course.name}</td>
              <td>{course.professorName}</td>
              <td className="action-cell">
                {selectedCourses.includes(course.id) ? 
                  <span className="already-added">Ya está en la lista</span> : 
                  <button className="submit-button" onClick={() => handleCourseSelection(course.id)}>Agregar</button>}
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      {errorMessage && <p className="error-message">{errorMessage}</p>}
      <div className="button-group">
        <button className="back-button" onClick={onBack}>Volver</button>
      </div>
    </div>
  );

};

export default RegisterCourses;
