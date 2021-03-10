export default function Error(props) {
  const { message } = props;

  return <div className="text-red-500">Sorry, something has gone wrong: {message}</div>;
}
