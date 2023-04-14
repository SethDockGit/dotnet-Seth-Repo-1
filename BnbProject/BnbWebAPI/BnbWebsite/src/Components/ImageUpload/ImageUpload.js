import { Typography } from "@mui/material";
import Box from '@mui/material/Box';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import Grid from '@mui/material/Unstable_Grid2';
import DeleteForeverIcon from '@mui/icons-material/DeleteForever';

export default function ImageUpload({
  files,
  setFiles,
  handleClickRemoveFile
}){

const Files = () => {
    
  return files.map(function(val, index){
      return(      
          <ListItem key={index}>
              {val.file.name}
              <ListItemButton type="button" onClick={handleClickRemoveFile} data-value1={val.id}>
                <ListItemIcon>
                  <DeleteForeverIcon/>
                </ListItemIcon>
              </ListItemButton>
          </ListItem>           
      )        
  })
}

const fileSelectedHandler = (e) => {
    
  const arr = Array.from(e.target.files);

  if (files.length == 0){
      
      var filesToAdd = arr.map(function(val, index) {
          return(
              {
                  id: index,
                  file: val
              }
          )
      })
      setFiles([...filesToAdd]);
  }
  else{
      var ids = files.map(function(val) {
          return(
              val.id
          )
      })
      var highest = ids.reduce((a, b) => Math.max(a, b), -Infinity);

      var filesToAdd = arr.map(function(val, index) {
          return(
              {
                  id: highest + index + 1,
                  file: val
              }
          )
      })
      setFiles([...files, ...filesToAdd]);
  }
}

  return (
    <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}>
      <form>
        <div><Typography variant="h6">Upload Images</Typography></div>
          <Box sx={{ width: '100%', maxWidth: 360, bgcolor: 'background.paper' }}>
              <List>
                <Files/>
              </List>
          </Box>
        <input type="file" multiple onChange={fileSelectedHandler} />
      </form>
    </Grid>
  )
}