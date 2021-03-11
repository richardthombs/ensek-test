import { useState } from "react";
import axios from "axios";

import Error from "../components/Error";
import ResultsSummary from "../components/ResultsSummary";
import Results from "../components/Results";

export default function Home() {

  const [uploadEnabled, setUploadEnabled] = useState(false);
  const [csv, setCsv] = useState("");
  const [error, setError] = useState(null);
  const [results, setResults] = useState(null);

  function handleUpload(e) {
    setUploadEnabled(false);

    axios.post(`${process.env.NEXT_PUBLIC_API}/api/meter-reading-uploads`, csv, { headers: { "content-type": "text/csv" } })
      .then(x => {
        setResults(x.data);
        setError(null);
      })
      .catch(e => {
        setResults(null);
        setError(e);
        setUploadEnabled(true);
      });
  }

  function handleCsvInput(e) {
    setUploadEnabled(e.target.value);
    setCsv(e.target.value);
  }

  function handleReset(e) {
    if (!confirm("Do you really want to reset all the meter readings?")) return;

    axios.post(`${process.env.NEXT_PUBLIC_API}/api/reset`).then(x => {
      setError(null);
      setResults(null);
      setCsv("");
    });
  }

  return (
    <div className="min-w-screen min-h-screen bg-ensek-blue text-white">
      <div className="container mx-auto p-16">
        <header>
          <h1 className="text-3xl">Ensek Software Engineer Remote Technical Exercise</h1>
          <h2 className="text-2xl"><span className="opacity-50">by </span><a href="https://agilesnowball.com/cv">Richard Thombs</a></h2>
        </header>

        <main className="mt-16">

          <label htmlFor="csv" className="text-xl block opacity-50 mb-2">Meter reading CSV:</label>
          <textarea onInput={handleCsvInput} name="csv" id="csv" className="text-black w-full h-64 p-2" placeholder="Please paste some meter reading CSV into here" value={csv}></textarea>

          <div className="flex items-center mt-4 space-x-4">
            <div className="w-full">
              {error && <Error message={error?.message} />}
              {results && <ResultsSummary results={results} />}
            </div>
            <button onClick={handleReset} className="border border-red-500 text-red-500 rounded px-4 py-2 flex-shrink-0">Reset database</button>
            <button disabled={!uploadEnabled} onClick={handleUpload} className="bg-ensek-orange rounded disabled:bg-gray-400 disabled:cursor-not-allowed text-white px-4 py-2">Upload</button>
          </div>

          {results && <Results results={results} />}

        </main>

      </div>
    </div >
  );
}
