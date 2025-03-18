// client-app/src/components/ChatView.jsx
import React, { useState, useRef, useEffect } from "react";
import Box from "@mui/material/Box";
import TextField from "@mui/material/TextField";
import SendIcon from "@mui/icons-material/Send";
import Typography from "@mui/material/Typography";
import Avatar from "@mui/material/Avatar";
import Paper from "@mui/material/Paper";
import { deepOrange, lightBlue } from "@mui/material/colors";
import { IconButton } from "@mui/material";
import { useLocation } from "react-router-dom";
import { getAICompletion } from "../services/api"; // Import the API function

function ChatView() {
  const location = useLocation();
  const { formData } = location.state || { formData: {} };

  const [messages, setMessages] = useState([]);
  const [newMessage, setNewMessage] = useState("");
  const messagesEndRef = useRef(null);

  useEffect(() => {
    const initializeChat = async () => {
      if (formData && Object.keys(formData).length > 0) {
        const initialAIMessage = `تم استلام بيانات النموذج:\n${JSON.stringify(
          formData,
          null,
          2
        )}`;

        // Send initial message to the AI
        try {
          const aiResponse = await getAICompletion(initialAIMessage);
          console.log("AI Response:", aiResponse);
          setMessages([
            { text: initialAIMessage, sender: "user" }, // Show submitted form data
            { text: aiResponse.data, sender: "ai" }, // Assuming the API returns { message: "..." }
          ]);
        } catch (error) {
          setMessages([
            { text: initialAIMessage, sender: "user" }, // Show submitted form
            { text: "Error getting AI response.", sender: "ai" }, //error
          ]);
        }
      }
    };
    initializeChat();
  }, [formData]);

  const handleSendMessage = async () => {
    if (newMessage.trim() !== "") {
      // Add the user's message to the chat
      setMessages((prevMessages) => [
        ...prevMessages,
        { text: newMessage, sender: "user" },
      ]);
      setNewMessage("");

      // Send the message to the AI and get the response
      try {
        const aiResponse = await getAICompletion(newMessage);
        setMessages((prevMessages) => [
          ...prevMessages,
          { text: aiResponse.res, sender: "ai" }, // Assuming the API returns { message: "..." }
        ]);
      } catch (error) {
        console.error("Error in handleSendMessage:", error);
        setMessages((prevMessages) => [
          ...prevMessages,
          { text: "Error getting AI response.", sender: "ai" },
        ]);
      }
    }
  };

  const handleInputChange = (event) => setNewMessage(event.target.value);

  const handleKeyPress = (event) => {
    if (event.key === "Enter" && !event.shiftKey) {
      event.preventDefault();
      handleSendMessage();
    }
  };

  useEffect(() => {
    scrollToBottom();
  }, [messages]);

  const scrollToBottom = () => {
    messagesEndRef.current?.scrollIntoView({ behavior: "smooth" });
  };

  return (
    <Box sx={{ height: "100%", display: "flex", flexDirection: "column" }}>
      <Typography
        variant="h5"
        sx={{
          mb: 3,
          fontWeight: 600,
          color: "primary.main",
        }}
      >
        محادثة
      </Typography>

      <Paper
        elevation={1}
        sx={{
          flex: 1,
          overflowY: "auto",
          p: 3,
          mb: 2,
          bgcolor: "white",
          borderRadius: 2,
          maxHeight: "calc(100vh - 200px)",
        }}
        dir="ltr"
      >
        {messages.map((message, index) => (
          <Box
            key={index}
            sx={{
              display: "flex",
              alignItems: "flex-end",
              justifyContent:
                message.sender === "user" ? "flex-end" : "flex-start",
              mb: 2,
            }}
          >
            {message.sender === "ai" && (
              <Avatar
                sx={{ bgcolor: lightBlue[500], mr: 1, width: 32, height: 32 }}
              >
                ذكاء
              </Avatar>
            )}
            <Paper
              elevation={0}
              sx={{
                p: 2,
                maxWidth: "70%",
                borderRadius: 3,
                color: message.sender === "user" ? "grey.100" : "grey.800",
                bgcolor:
                  message.sender === "user" ? "primary.light" : "grey.100",
                boxShadow: "0 1px 3px rgba(0,0,0,0.1)",
              }}
            >
              <Typography variant="body1" sx={{ wordBreak: "break-word" }}>
                {message.text}
              </Typography>
            </Paper>
            {message.sender === "user" && (
              <Avatar
                sx={{ bgcolor: deepOrange[500], ml: 1, width: 32, height: 32 }}
              >
                أنا
              </Avatar>
            )}
          </Box>
        ))}
        <div ref={messagesEndRef} />
      </Paper>

      <Box sx={{ display: "flex", alignItems: "center", gap: 2 }}>
        <TextField
          fullWidth
          variant="outlined"
          placeholder="اكتب رسالتك..."
          value={newMessage}
          onChange={handleInputChange}
          onKeyPress={handleKeyPress}
          sx={{
            "& .MuiOutlinedInput-root": {
              borderRadius: 12,
              bgcolor: "white",
              boxShadow: "0 1px 3px rgba(0,0,0,0.1)",
            },
          }}
          InputProps={{
            endAdornment: (
              <IconButton
                color="primary"
                onClick={handleSendMessage}
                disabled={!newMessage.trim()}
              >
                <SendIcon />
              </IconButton>
            ),
          }}
        />
      </Box>
    </Box>
  );
}

export default ChatView;
