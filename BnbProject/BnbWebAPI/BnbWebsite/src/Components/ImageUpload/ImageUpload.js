import { useState } from "react";
import { Typography } from "@mui/material";


export default function ImageUpload(){

const [files, setFiles] = useState([]);

const fileSelectedHandler = (e) => {
  setFiles([...files, ...e.target.files]);
  debugger;
}

  return (
    <form>
      <div><Typography variant="subtitle1">Upload Images</Typography></div>
      <input type="file" multiple onChange={fileSelectedHandler} />
    </form>
  )
}