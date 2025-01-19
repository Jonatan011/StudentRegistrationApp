import { useState } from 'react';
import axios from 'axios';
import './RegisterStudent.css';
import { useNavigate } from 'react-router-dom';

const RegisterStudent = () => { 
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [email, setEmail] = useState('');
  const navigate = useNavigate();


  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const studentData = { firstName, lastName, email };
      const response = await axios.post('http://localhost:5275/api/students', studentData);
      const studentId = response.data.id; // Obteniendo el ID del estudiante recién creado
      navigate(`/students/${studentId}/courses`); // Redirigiendo a la página de cursos
    } catch (error) {
      console.error('Error al registrar estudiante:', error);
    }
  };

  return (
    <div className="form-container">
      <h2 className="form-title">Registrar Estudiante</h2>
      <form onSubmit={handleSubmit}>
        <div className="form-group">
          <label>Nombre:</label>
          <input type="text" value={firstName} onChange={(e) => setFirstName(e.target.value)} />
        </div>
        <div className="form-group">
          <label>Apellido:</label>
          <input type="text" value={lastName} onChange={(e) => setLastName(e.target.value)} />
        </div>
        <div className="form-group">
          <label>Email:</label>
          <input type="email" value={email} onChange={(e) => setEmail(e.target.value)} />
        </div>
        <button type="submit">Registrar Estudiante</button>
      </form>
    </div>
  );
};


export default RegisterStudent;
